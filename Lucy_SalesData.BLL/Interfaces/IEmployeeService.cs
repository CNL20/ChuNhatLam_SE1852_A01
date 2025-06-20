﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Models;

namespace Lucy_SalesData.BLL.Interfaces
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
        Employee Login(string username, string password);
    }
}
