   M S B A M L   `   `   `  ����G  CStockManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:StockManagerStockManager  5          StockManager.MainWindow   5   	   n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,localclr-namespace:StockManager     5      -   5          � Title$  Stock Manager��5   	    � WindowStartupLocation$ CenterScreen=�6    |�.  � 
ResizeMode$ NoResize=�6�    � WindowState$ Normal=�5   	   ��6   �� *&$SystemParameters.PrimaryScreenHeight+��6W   �� *%#SystemParameters.PrimaryScreenWidth+ � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5	      �� 5
      $ ../DBResources.xaml?�6%   %    5      .��5      � .+�5   
   �  � ColumnDefinitions 5      �� 5      6%   �� 5      $	��65* �6#   61   �� 5      6%    � RowDefinitions 5      �� 5      6"   �� 5      $��5* �6    6.   �� 5      6"   .+�5      �  5      �� 5      6&   �� 5      6&   5      �� 5"      -   5        $��	lbl_Title��5"   p   $��1��6   $��Center=�6(   ���#00a5c2	 �� TextAlignment$	 Center=�6Y   $��Welcome��6�   $��36(�6�   6�   � 5#      $��1��6   $��1��6$    5$      �� 5%      6)   �� 5&      $
��auto �6'   66   �� 5'      6)   .+�5)      � -   5        $��panel_pages��5)      $��1��6*   $��1��6:   5+      5,      � 5-      $��2��6   $��1��6     5.      �� 5/      6)   �� 50      6)   �� 51      6)   .+�53      �� -   5        $��btnHome��53         Style_BtnHome#��[  6*   

 � Tag$

 Home��6n   $	��Top=�6z   $��Stretch=�6�   .��54      � $��Stretch=�6   $��Stretch=�67   .+�55      �� $
��Home��6$     style_navigTextBlock#��[ 60   $��Stretch=�6^   $��Stretch=�6z   6�   56      57      �� 58      -   5        $��btnBack��58      $	��Top=�6)   #��[  6B   $

 Back��6i   $��1��6�   $��Stretch=�6�   .��59      � .+�5:      �� -   5        $��Txt_Back��5:   $   $
��Back��64   #��[ 6@   $��Stretch=�6n   $��Stretch=�6�   6�   5;      5<      �� 5=      -   5        $��btnNext��5=      $	��Top=�6(   #��[  6@   $

 Next��6�   $��3��6�   $��Stretch=�6�   .��5>      � .+�5?      �� -   5        $��lbl_next��5?   0   $
��Next��6$   #��[ 6@   $	��Top=�6n   $��Stretch=�6�   6�   5@      5A      5C      5D      5E      5F      