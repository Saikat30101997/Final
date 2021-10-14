using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class Logger
    {
        private static Logger _logger;

        public string TestValue { get; set; }
        private Logger() // ei private constructor create korar karon e baire thke kew r instace create korte parbe na
        {
            TestValue = DateTime.Now.ToString();
        }

        //ensure korbo j eta ektar besi instance create korbo na
        public static Logger CreateLogger()
        {
            if (_logger == null)
                _logger = new Logger();   // ei duita line multithread create korte hobe naile inconsistency create krbe

            return _logger;
        
        }


      
    }
}
