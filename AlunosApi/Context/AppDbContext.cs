﻿using AlunosApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Aluno> Alunos { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Aluno>().HasData(new Aluno
        //    {
        //        Id = 1,
        //        Nome = "Maria da Penha",
        //        Email = "maria@gmail.com",
        //        Idade = 23
        //    },
        //    new Aluno
        //    {
        //        Id = 2,
        //        Nome = "Cesar Alves",
        //        Email = "cesar@gmail.com",
        //        Idade = 24
        //    });
        //}
    }
}
