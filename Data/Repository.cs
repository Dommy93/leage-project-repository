using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WillThisWork.Models;

namespace WillThisWork.Data
{
    public class Repository
    {

        public ApplicationDbContext _context = null;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public SelectList getChampList()
        {
            
            return new SelectList(_context.Champions.ToList());
        }

    }
}