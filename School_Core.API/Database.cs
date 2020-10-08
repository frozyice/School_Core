using System.Collections.Generic;
using School_Core.API.Model;

namespace School_Core.API
{
    public class Database
    {
        public static Database store { get;  } = new Database(); 
        
        public List<City> citys = new List<City>
        {
            new City{Id=1, Name="Tallinn"},
            new City{Id=2, Name="Tartu"}
        };
    }
}