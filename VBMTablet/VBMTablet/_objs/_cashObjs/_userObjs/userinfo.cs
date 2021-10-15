using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._objs._userObjs
{
    public class userinfo
    {
        public string UserName { get; set; }
        public int UserID { get; set; }
        public DateTime Birthday { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public string FaceId { get; set; }
        public string Fullname { get; set; }
        public string PhoneNo { get; set; }
        public int Point { get; set; }
        public string UserLevel { get; set; }
        public int SoBanhTichLuy { get; set; }
        public int SoQuaTichLuyConLai { get; set; }
        public string AvatarImage { get; set; }
        public int UserStatus { get; set; }
        public int IndexVBMEmp { get; set; }
        public int ComboNV { get; set; }
        public string KudovaUserID { get; set; }
        public bool activeKudova { get; set; }
        public bool isSurvey { get; set; }
        public bool isSurveySpagheti { get; set; }
        public bool is_has_birthday { get; set; }
        public string special_voucher { get; set; }
        public Dictionary<string,string> ddStatus { get; set; }
        public string ddStatusVN
        {
            get
            {
                return tools.getVNName(ddStatus);
            }
        }
        public List<object> wow_histories { get; set; }
        
        public userinfo clone()
        {
            var rt = new userinfo
            {
                UserName = UserName,
                UserID = UserID,
                UserLevel = UserLevel,
                UserStatus = UserStatus,
                KudovaUserID = KudovaUserID,
                activeKudova = activeKudova,
                AvatarImage = AvatarImage,
                Birthday = Birthday,
                ComboNV = ComboNV,
                ddStatus = new Dictionary<string, string>(ddStatus),
                Email = Email,
                FaceId = FaceId,
                Fullname = Fullname,
                Gender = Gender,
                IndexVBMEmp = IndexVBMEmp,
                isSurvey = isSurvey,
                isSurveySpagheti = isSurveySpagheti,
                is_has_birthday = is_has_birthday,
                PhoneNo = PhoneNo,
                Point = Point,
                SoBanhTichLuy = SoBanhTichLuy,
                SoQuaTichLuyConLai = SoQuaTichLuyConLai,
                special_voucher = special_voucher,
                wow_histories = new List<object>(),
            };
            wow_histories.ForEach(p => rt.wow_histories.Add(p));
            return rt;
        }
        public static async Task<userinfo> getUserData(string sdt)
        {
            string url = $"{localdb.endpoin}full_user_info?sdt={sdt}";
            if (tools.isConn())
            {
                try
                {
                    using (var cl = tools.createHttpClient())
                    {
                        var resp = await cl.GetAsync(url);
                        var data = await resp.Content.ReadAsStringAsync();
                        var jOb = JObject.Parse(data);
                        var isSuccess = bool.Parse(tools.GetJArrayValue(jOb, "Success"));
                        if (isSuccess)
                        {
                            var str = tools.GetJArrayValue(jOb, "Data");
                            var res = JsonConvert.DeserializeObject<userinfo>(str);
                            localdb.userinfo = res;
                            return res;
                        }
                    }
                }
                catch (Exception e)
                {
                    // o day can log lai su kien
                }
            }
            else
            {
                // o day can log lai su kien
            }
            return null;
        }

    }
}
