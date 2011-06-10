using System;
using Jukebox.Business.Models.Contracts;

namespace Jukebox.Business.Models
{
    public class User :  IPersistable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
    }
}