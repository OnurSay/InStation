using System.Collections.Generic;
using InstationFinalVersion.Models;

namespace InstationFinalVersion.DataAccess
{
    internal class MySqlClonnection : List<FollowingTable>
    {
        private string v;

        public MySqlClonnection(string v)
        {
            this.v = v;
        }
    }
}