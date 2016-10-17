using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using DataModels;
using PathViewModels;
using PathDataModels;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Path
{
    public static class App
    {
        static App()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new MockSchoolService()).As<ISchoolService>();
            builder.RegisterType(typeof(SchoolSelectionViewModel));
            builder.RegisterType(typeof(ClassSelectionViewModel));

            Container = builder.Build();
        }

        public static IContainer Container { get; set; }
    }
}