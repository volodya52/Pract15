using Pract15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract15.Service
{
    public class DbService
    {
        private Pract15DatabaseContext context;
        public Pract15DatabaseContext Context => context;
        private static DbService? instance;
        public static DbService Instance
        {
            get
            {
                if(instance == null)
                    instance = new DbService();
                return instance;
            }
        }

        private DbService()
        {
            context = new Pract15DatabaseContext();
        }
    }
}
