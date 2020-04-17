using Application.IServices;
using Application.Models.User;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ZnoService : IZnoService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public ZnoService(UserManager<User> userManager, IHttpContextAccessor httpContext, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _httpContext = httpContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> SaveZNOMarks(AddMarksModel model)
        {
            var userName = _httpContext.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var result = false;

            if (user.ZNOId == null)
                result = await SetZnoMarks(model, true, user);
            else
                result = await SetZnoMarks(model, false, user);

            return result;
        }

        private async Task<bool> SetZnoMarks(AddMarksModel model, bool isNewZno, User user)
        {
            ZNO zno;
            if (isNewZno)
            {
                zno = new ZNO();
                user.ZNOId = zno.Id;
                zno.User = user;
            }
            else
            {
                zno = await _unitOfWork.ZNORepository.GetAsync(user.ZNOId.Value);
            }

            if (model.FirstZnoModel != null)
                zno.SetMark(model.FirstZnoModel.Name, Convert.ToDouble(model.FirstZnoModel.Mark));

            if (model.SecondZnoModel != null)
                zno.SetMark(model.SecondZnoModel.Name, Convert.ToDouble(model.SecondZnoModel.Mark));

            if (model.ThirdZnoModel != null)
                zno.SetMark(model.ThirdZnoModel.Name, Convert.ToDouble(model.ThirdZnoModel.Mark));

            if (model.FourZnoModel != null && model.FourZnoModel.Name != "None")
                zno.SetMark(model.FourZnoModel.Name, Convert.ToDouble(model.FourZnoModel.Mark));

            if (isNewZno) await _unitOfWork.ZNORepository.CreateAsync(zno);

            var result = await _unitOfWork.Commit();

            return result == 1;
        }
    }
}