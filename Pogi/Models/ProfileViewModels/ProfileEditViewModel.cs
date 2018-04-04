using Microsoft.AspNetCore.Http;
using Pogi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pogi.Models.ProfileViewModels
{
    public class ProfileEditViewModel : Member
    {
        public IFormFile AvatarImage { get; set; }
    }
}
