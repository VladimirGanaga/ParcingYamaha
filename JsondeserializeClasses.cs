﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;


namespace ParcingYamaha
{

    public class JsondeserializeClasses
    {
        public Menudata menuData { get; set; }
        public Productdatacollection[] productDataCollection { get; set; }
        public Modelnamedatacollection[] modelNameDataCollection { get; set; }
        public Displacementdatacollection[] displacementDataCollection { get; set; }
        public object modelYearDataCollection { get; set; }
        public Usercontext userContext { get; set; }
    }

    public class Model
    {
        public Modelnamedatacollection[] modelNameDataCollection { get; set; }
    }

    public class Menudata
    {
        public object relatedLinkDataCollection { get; set; }
        public bool contactUsFlag { get; set; }
        public string contactUsURL { get; set; }
        public string helpURL { get; set; }
        public bool relatedLinkFlag { get; set; }
    }

    public class Usercontext
    {
        public string dateFormat { get; set; }
        public bool termOfUseFlag { get; set; }
        public string termOfUseURL { get; set; }
        public string destination { get; set; }
        public bool useProdCategory { get; set; }
        public string userGroupCode { get; set; }
        public string destGroupCode { get; set; }
        public bool greyModelSign { get; set; }
        public string domOvsId { get; set; }
        public string cataPBaseCode { get; set; }
        public string priceDisplayType { get; set; }
        public bool stockDisplay { get; set; }
        public int pickListPartCnt { get; set; }
        public string boundSymbol { get; set; }
        public bool taxDisplay { get; set; }
        public bool mobileAvailableFlag { get; set; }
        public string currencyCode { get; set; }
    }

    public class Productdatacollection
    {
        public string vinNoLabel { get; set; }
        public string vinNoLabel2 { get; set; }
        public string modelLabelSearchHelpURL { get; set; }
        public string vinNoGuideMessage { get; set; }
        public string vinNoSearchHelpURL { get; set; }
        public string vinNoSearchLabel { get; set; }
        public string productId { get; set; }
        public string modelLabelSearchId { get; set; }
        public string modelNameSearchId { get; set; }
        public string productIdName { get; set; }
        public string vinNoSearchId { get; set; }
    }

    public class Modelnamedatacollection
    {
        public object? nickname { get; set; }
        public string? modelName { get; set; }
        public string? dispModelName { get; set; }
        public string? productId { get; set; }
    }

    public class Displacementdatacollection
    {
        public string productId { get; set; }
        public string displacementType { get; set; }
        public string displacement { get; set; }
    }


    public class Modelyeardata
    {
        public Modelyeardatacollection[] modelYearDataCollection { get; set; }
    }

    public class Modelyeardatacollection
    {
        public string modelYear { get; set; }
        public string productId { get; set; }
    }


    public class Modeldatacollections
    {
        public int id { get; set; }
        public Modeldatacollection[] modelDataCollection { get; set; }
    }

    public class Modeldatacollection
    {
        public int Id { get; set; }
        public string prodPictureFileURL { get; set; }
        public string? nickname { get; set; }
        public string modelName { get; set; }
        public string modelYear { get; set; }
        public string? modelTypeCode { get; set; }
        public string productNo { get; set; }
        public string? colorType { get; set; }
        public string? modelBaseCode { get; set; }
        public string? colorName { get; set; }
        public string? calledCode { get; set; }
        public bool? vinNoSearch { get; set; }
        public string? prodPictureNo { get; set; }
        public string? releaseYymm { get; set; }
        public string? modelComment { get; set; }
        public string? prodCategory { get; set; }
    }
    public class SampleContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=WIN10\\SQLEXPRESS;Database=Yamaha;Trusted_Connection=True;TrustServerCertificate=true;");
        }
        public SampleContext()
        {
            Database.EnsureDeleted();   // удаляем бд со старой схемой
            Database.EnsureCreated();   // создаем бд с новой схемой
        }

        public DbSet<Modeldatacollection> Modeldatacollection { get; set; }
        
    }


    
}

