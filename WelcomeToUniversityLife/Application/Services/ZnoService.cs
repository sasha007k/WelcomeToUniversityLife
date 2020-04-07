using Application.Models.User;
using Domain.Entities;
using Application.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Services
{
    class ZnoService : IZnoService
    {
        public Task<bool> Create(User user, AddMarksModel model)
        {
            Zno zno = new Zno();
            PropertyInfo[] properties = typeof(Zno).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if(property.Name==model.FirstZno.Name)
                {
                    
                }
                if (property.Name == model.SecondZno.Name)
                {

                }
                if (property.Name == model.ThreedZno.Name)
                {

                }
                if("None"!= model.FourZno.Name)
                {
                    if (property.Name == model.FourZno.Name)
                    {

                    }
                }
               
            }

        }
    }
}
