﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace 银行储蓄管理
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BankDbEntities2 : DbContext
    {
        public BankDbEntities2()
            : base("name=BankDbEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Costomer> Costomer { get; set; }
        public virtual DbSet<Deposit> Deposit { get; set; }
        public virtual DbSet<InterestRate> InterestRate { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Summary> Summary { get; set; }
    }
}
