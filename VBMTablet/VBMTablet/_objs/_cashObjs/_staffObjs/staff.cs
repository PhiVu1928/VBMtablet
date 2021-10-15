using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VBMTablet._objs._staffObjs
{
    public class staff
    {
        long _userid;
        string _username;
        string _pwd;
        string _phoneno;
        string _email;
        DateTime _joindate;
        string _userlastact;
        string _usercurtoken;
        DateTime _lastactdate;
        string _rolejson;
        string _infojson;
        string _hoten;
        DateTime _birthday;
        int _combo30;
        int _loyalty;
        string _notiTokens;
        string _maNV;
        int _combonv;
        int _isNV;
        double _salaryByHour;

        public int NVID { get; set; }

        public int IsNV
        {
            get { return _isNV; }
            set { _isNV = value; }
        }

        public double SalaryByHour
        {
            get { return _salaryByHour; }
            set { _salaryByHour = value; }
        }

        public int comboNV
        {
            get { return _combonv; }
            set { value = _combonv; }
        }

        public string MaNV
        {
            get { return _maNV; }
            set { _maNV = value; }
        }

        public int Loyalty
        {
            get { return _loyalty; }
            set { _loyalty = value; }
        }

        public string NotiTokens
        {
            get { return _notiTokens; }
            set { _notiTokens = value; }
        }

        public int Combo30
        {
            get { return _combo30; }
            set { _combo30 = value; }
        }

        public long UserID
        {
            get { return _userid; }
            set { _userid = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        public string PhoneNo
        {
            get { return _phoneno; }
            set { _phoneno = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DateTime joinDate
        {
            get { return _joindate; }
            set { _joindate = value; }
        }

        public string userLastAct
        {
            get { return _userlastact; }
            set { _userlastact = value; }
        }

        public string userCurToken
        {
            get { return _usercurtoken; }
            set { _usercurtoken = value; }
        }

        public DateTime lastActDate
        {
            get { return _lastactdate; }
            set { _lastactdate = value; }
        }

        public string RoleJSON
        {
            get { return _rolejson; }
            set { _rolejson = value; }
        }

        public string InfoJSON
        {
            get { return _infojson; }
            set { _infojson = value; }
        }

        public string HoTen
        {
            get { return _hoten; }
            set { _hoten = value; }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
        int _roleID;
        public int roleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }


        public int points;
        //public int roleID;
        public staff()
        {
        }
        public static List<staff> parseNV(string data)
        {
            List<staff> lst = new List<staff>();
            try
            {
                string[] arr = data.Split('$');

                staff obj;
                foreach (string s in arr)
                {
                    string[] arrMe = s.Split('#');
                    obj = new staff();
                    obj.UserID = long.Parse(arrMe[0]);
                    obj.Username = arrMe[1];
                    obj.HoTen = arrMe[2];
                    obj.Pwd = arrMe[7];

                    lst.Add(obj);
                }
            }
            catch { }

            return lst;
        }


    }
}
