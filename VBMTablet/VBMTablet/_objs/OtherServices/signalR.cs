using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using VBMTablet._pages;
using Xamarin.Forms;
using VBMTablet._process;
using VBMTablet._objs._staffObjs;
using VBMTablet._objs._cashObjs._barcodeObjs;

/// <summary>
/// Day la class dam bao viec ket noi giua server va client
/// Khi vao app khoi tao new class signalR() va luu lai, sau do goi lenh ConnectAsync de thuc hien ket noi
/// Muon de nghi server 1 cai j do, goi toi HubProxy de invoke
/// vi du muon lay tat ca danh sach nhan vien: HubProxy.Invoke("SystemAction", "doListAllNV{}"+shopID+ "$0{}");
/// client se listen "doListAllNV" (nhu ben duoi do server tra ve). Toc do nhanh hon khi dung api nua (diem yeu la mang ko on dinh dex mat ket noi nen da them ham reconnect cho no roi)
/// </summary>

namespace VBMTablet._objs.OtherServices
{

    public class signalR
    {

        public signalR()
        {

        }

        public IHubProxy HubProxy { get; set; }
        const string ServerURI = "http://vuabanhmi.com:1984/signalr";
        private HubConnection Connection { get; set; }
        public bool isConn = false;
        public bool isConnting = false;

        public async void tryToReconnect()
        {
            if (!isConn)
            {
                try
                {
                    isConnting = true;
                    await Connection.Start();
                    await HubProxy.Invoke("UserAction", "getConnectionId{}_{}_");
                }
                catch { }
            }
        }

        private void Connection_Closed()
        {
            isConn = false;
            if (!isConnting)
            {
                tryToReconnect();
            }
        }

        private void Connection_ConnectionSlow()
        {

        }

        private void Connection_Reconnected()
        {
            
        }

        public async void ConnectAsync()
        {
            try
            {
                Connection = new HubConnection(ServerURI);
                Connection.Closed += Connection_Closed;
                Connection.ConnectionSlow += Connection_ConnectionSlow;
                Connection.Reconnected += Connection_Reconnected;

                HubProxy = Connection.CreateHubProxy("MyHub");

                HubProxy.On<string>("UserAction", (message) =>
                   processMsg(message)
               );
                HubProxy.On<string>("invalidRequest", (message) =>
                    processMsg("invalidRequest{}_{}_")
                );
                HubProxy.On<string>("SystemAction", (message) =>
                    processMsgSys(message)
                );
                HubProxy.On<string>("UserList", (message) =>
                    processMsgUserList(message)
                );
                HubProxy.On<string>("BillList", (message) =>
                    processMsgBillList(message)
                );
                HubProxy.On<string>("BillAction", (message) =>

                    processMsgBillAction(message)
                );

                isConnting = true;

                await Connection.Start();

                await HubProxy.Invoke("UserAction", "getConnectionId{}_{}_");
            }
            catch (HttpRequestException e)
            {
                return;
            }
        }

        public async void processMsg(string msg)
        {
            try
            {
                string[] arrM = msg.Split(new string[] { "{}" }, StringSplitOptions.None);
                string _func = arrM[0];
                string _data = arrM[1];
                string _encoded = arrM[2];

                if (_func == "getConnectionId")
                {
                    isConn = true;
                    isConnting = false;
                }
            }
            catch (Exception e)
            {

            }
        }

        public void processMsgSys(string msg)
        {
            try
            {
                string[] arrM = msg.Split(new string[] { "{}" }, StringSplitOptions.None);
                string _func = arrM[0];
                string _data = arrM[1];
                string _encoded = arrM[2];

                switch (_func)
                {
                    case "doListAllNV":
                        {
                            //E gui _dât sang bên ham kia de parse thanh obj thông tin nhân viên
                            var data = staff.parseNV(_data);
                            if(data != null)
                            {
                                localdb.FullNhanVienInfo = data;
                            }
                            break;
                        }
                    case "getBarCodeInfo":
                        {
                            var data = nlBarcode.parseBarCode(_data);
                            if(data != null)
                            {
                                try
                                {
                                    //Check hsd
                                    DateTime d = DateTime.Now;
                                    TimeSpan ts = d.Subtract(data.NgayHetHan);
                                    long tg = ts.Days * 1440 + ts.Hours * 60 + ts.Minutes;
                                    if (tg <= 30)
                                    {
                                        Device.BeginInvokeOnMainThread(() =>
                                        {
                                            Application.Current.MainPage.DisplayAlert("", "Pack này đã hết hạn sử dụng!", "OK");
                                        });
                                        return;
                                    }
                                    //check shopid
                                    if(data.ShopID != localdb.shopID)
                                    {
                                        Device.BeginInvokeOnMainThread(() =>
                                        {
                                            Application.Current.MainPage.DisplayAlert("", "Pack này không thuộc store này, vui lòng báo IT!", "OK");
                                        });
                                        return;
                                    }
                                    localdb.nlBarcode = data;
                                    //Kiểm tra xem có barcode nào cùng NLID mà có hsd trước cái này không
                                    localdb.signalR.HubProxy.Invoke("SystemAction", "doCheckBarCodeValid{}" + data.Code + "$" + data.NgayHetHan.ToString("MM-dd-yyyy") + "$" + data.NLID + "$" + data.ShopID + "{}");

                                }
                                catch
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        Application.Current.MainPage.DisplayAlert("", "Barcode này có lỗi khi xử lý, vui lòng báo lại cho IT", "OK");
                                    });
                                }
                            }
                            break;
                        }
                    case "doCheckBarCodeValid":
                        {
                            if(_data.IndexOf("OK") > -1)
                            {
                                localdb.usingNLPage.renderData();
                            }
                            else
                            {
                                string[] arr = _data.Split('$');
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    Application.Current.MainPage.DisplayAlert("", "Có" + arr[1] + "pack có shelflife trước pack này, vui lòng kiểm tra lại!", "OK");
                                });
                                localdb.nlBarcode = null;
                            }
                            break;
                        }
                    case "doInsertBarCodeHis":
                        {
                            if(_data.IndexOf("ERR") > -1)
                            {
                                string[] arr = _data.Split('$');
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    Application.Current.MainPage.DisplayAlert("", arr[1] , "OK");
                                });
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    Application.Current.MainPage.DisplayAlert("", "Lưu thành công", "OK");
                                });
                                localdb.usingNLPage.actionAfterSave();
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch { }
        }

        public void processMsgUserList(string msg)
        {

        }

        public void processMsgBillList(string msg)
        {

        }

        public void processMsgBillAction(string msg)
        {
            
        }

    }
}
