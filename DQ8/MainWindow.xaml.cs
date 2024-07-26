﻿using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DQ8
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		DataContext mDatacontext;

		public MainWindow()
		{
			Info.Instance();
			InitializeComponent();
		}

		private void Window_PreviewDragOver(object sender, DragEventArgs e)
		{
			e.Handled = e.Data.GetDataPresent(DataFormats.FileDrop);
		}

		private void Window_Drop(object sender, DragEventArgs e)
		{
			String[] files = e.Data.GetData(DataFormats.FileDrop) as String[];
			if (files == null) return;
			if (!System.IO.File.Exists(files[0])) return;

			SaveData.Instance().Open(files[0]);
			Init();
			MessageBox.Show("读取成功");
		}

		private void MenuItemFileOpen_Click(object sender, RoutedEventArgs e)
		{
			Load();
		}

		private void MenuItemFileSave_Click(object sender, RoutedEventArgs e)
		{
			Save();
		}

		private void MenuItemFileSaveAs_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			if (dlg.ShowDialog() == false) return;

			if (SaveData.Instance().SaveAs(dlg.FileName) == true) MessageBox.Show("写入成功");
			else MessageBox.Show("写入失败");
		}

		private void MenuItemExit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
		{
			new AboutWindow().ShowDialog();
		}

		private void MenuItemRecipeCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var recipe in mDatacontext.Recipes)
			{
				recipe.Exist = true;
			}
		}

		private void MenuItemRecipeUnCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var recipe in mDatacontext.Recipes)
			{
				recipe.Exist = false;
			}
		}

		private void ToolBarFileOpen_Click(object sender, RoutedEventArgs e)
		{
			Load();
		}

		private void ToolBarFileSave_Click(object sender, RoutedEventArgs e)
		{
			Save();
		}

		private void ToolBarAdventure_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox comp = sender as ComboBox;
			if (comp == null) return;
			SaveData.Instance().Adventure = (uint)comp.SelectedIndex;
			Init();
		}

		private void MenuItemPlaceCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var place in (DataContext as DataContext)?.Places)
			{
				place.Arrival = true;
			}
		}

		private void MenuItemPlaceUnCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var place in (DataContext as DataContext)?.Places)
			{
				place.Arrival = false;
			}
		}

		private void MenuItemItemCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var item in (DataContext as DataContext)?.Items)
			{
				item.Get = true;
			}
		}

		private void MenuItemItemUnCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var item in (DataContext as DataContext)?.Items)
			{
				item.Get = false;
			}
		}

		private void ButtonItemChange_Click(object sender, RoutedEventArgs e)
		{
			BagItem item = (sender as Button)?.DataContext as BagItem;
			if (item == null) return;
			ItemSelectWindow dlg = new ItemSelectWindow();
			dlg.ID = item.ID;
			dlg.ShowDialog();
			item.ID = dlg.ID;
			if (item.ID != 0) item.Count = 1;
		}

		private void ButtonCharactorItemChange_Click(object sender, RoutedEventArgs e)
		{
			CharactorItem item = (sender as Button)?.DataContext as CharactorItem;
			if (item == null) return;
			ItemSelectWindow dlg = new ItemSelectWindow();
			dlg.ID = item.ID;
			dlg.ShowDialog();
			item.ID = dlg.ID;
		}

		private void Load()
		{
			OpenFileDialog dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Open(dlg.FileName);
			Init();
			MessageBox.Show("读取成功");
		}

		private void Init()
		{
			mDatacontext = new DataContext();
			DataContext = mDatacontext;
		}

		private void Save()
		{
			if (SaveData.Instance().Save() == true) MessageBox.Show("写入成功");
			else MessageBox.Show("写入失败");
		}
	}
}
