using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WillThisWork.Data;
using WillThisWork.Models;

namespace WillThisWork.ViewModels
{
    public class UsersViewModel
    {


        public List<ApplicationUser> Users { get; set; } 

        public SelectList rolesSelectListItems { get; set; }
        public dynamic rolez { get; set; }
        public virtual SelectList GetSelectListItemsWithSelectedValue(string userId)
        {
            return new SelectList(rolez, "Id", "Name", userId);
        }

        public virtual void Init(Repository repository)
        {
            rolesSelectListItems = new SelectList(repository.getRoles().Items, "Id", "Name");
        }

        UpdateUserRoleViewModel Model { get; set; }

    }
}