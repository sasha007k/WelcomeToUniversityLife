using System.Threading.Tasks;
using Application.IServices;
using Application.Models.User;
using Domain.Entities;

namespace Application.Services
{
    internal class ZnoService : IZnoService
    {
        public Task<bool> Create(User user, AddMarksModel model)
        {
            var zno = new Zno();
            var properties = typeof(Zno).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == model.FirstZno.Name)
                {
                }

                if (property.Name == model.SecondZno.Name)
                {
                }

                if (property.Name == model.ThreedZno.Name)
                {
                }

                if ("None" != model.FourZno.Name && property.Name == model.FourZno.Name)
                {

                }
            }

            return null;
        }
    }
}