﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sunnah.ES
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class sunnahEntities : DbContext
    {
        public sunnahEntities()
            : base("name=sunnahEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<feedback> feedbacks { get; set; }
        public virtual DbSet<manager> managers { get; set; }
        public virtual DbSet<orderedproduct> orderedproducts { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}