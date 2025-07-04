﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChuNhatLamWPF.ViewModels;
using Lucy_SalesData.DAL.Repositories;
using Lucy_SalesData.DAL.Singleton;

namespace ChuNhatLamWPF.Views
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        public ProductView()
        {
            InitializeComponent();
            var context = DbContextFactory.Create();
            var productRepository = new ProductRepository(context);
            DataContext = new ProductViewModel(productRepository);
        }
    }
}
