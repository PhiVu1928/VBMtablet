using System;
using System.Collections.Generic;
using System.Text;

namespace VBMTablet._process
{
    public interface ISQLite
    {
        SQLite.SQLiteConnection GetConnection();
    }
}
