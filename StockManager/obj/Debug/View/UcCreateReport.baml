   M S B A M L   `   `   `  ����G  CStockManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:StockManagerStockManager  5      5clr-namespace:StockManager.ViewStockManager.View  5      &     StockManager.View.UcCreateReport   5      n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:StockManager     5      2staticclr-namespace:StockManager.View     5        � 	Resources  5       �� MergedDictionaries �� Source"   StockManager.View.StatusList   
StatusList�� 5   
    5      �� 5      $ ../DBResources.xaml?�6%   %   &	          5      65   5      .��5      � .+�5   
   ��  � DataContext 6   �� *MreportModel+.+�5      �� $��Center=�6   $��Center=�63   $
��auto��6P   $��25(�6^   $��	Underline{�6m   $��10q�6�   Choose Filters6�   �� 5      $��Center=�6   $
��Left=�63   $	��500��6N   $��20(�6\   $��5q�6k   Choose an item6�   �� 5      -   5         ��Name$ CmbBox��5   4   $��5q�6)   $	��200��6D   $
��Left=�6P   #��[  5   '   $��20(�6Q    �� SelectedValuePath$ Value��5    '    �� DisplayMemberPath$ Value��6A   ��5      Z�  �� SelectedValue 5!   '   �� *
ItemName+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� SelectedItem
 5"   '   �� *ItemNo+$ TwoWay=�$	 PropertyChanged=��� 5$      $��5q�6   $��Center=�6"    d� 	IsChecked 6=   �� *IsUseDateFilter+$ TwoWay=�$	 PropertyChanged=�.��6�   Use date filter6�   �� 5%       �� Orientation$ 
Horizontal=�6   $��5q�62   .+�5)      �� $��Center=�6   $
��Left=�67   $��5q�6S   Records from6l   (  "System.Windows.Controls.DatePicker  5*      $
��Left=�6      SelectedDate 69   �� *
DateFrom+$ TwoWay=�$	 PropertyChanged=� �� StringFormat$ dd MMM yyyy��6�   �� 5,      $
��Left=�6   $��Center=�68   $��5q�6U   
Records to6l     5-      $
��Left=�6    69   �� *DateTO+$ TwoWay=�$	 PropertyChanged=�$ dd MMM yyyy��6�   5.      50      51      52      