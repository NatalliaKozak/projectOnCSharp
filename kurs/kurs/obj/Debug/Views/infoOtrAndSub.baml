   M S B A M L   `   `   `  ����?  ;kurs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:kurs.Views
kurs.Views  5          kurs.Views.infoOtrAndSub   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:kurs.Views     5      .��5	      � .+�5
   
   �   � ColumnDefinitions  5      �� 5      $	��270 �6#   61   �� 5      6&   �� 5      $	��270 �6#   61    � RowDefinitions 5      �� 5      $��40 �6    6.   �� 5      $��40 �6    6.   �� 5      6"   �� 5      $��60 �6    6.   .+�5      � $��0��6   $��3��6    $��40��5      ��5      �� 5      .��5      �  � Color$ 	#FF091843��6'   � 5      $ 	#FF832B4E��6'    � Offset$ 1Q�69   5      .+�5"      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   .+�5#      �� $��/img/list_1.png��6   $
��None=�5$      $��	10 0 20 0q�5%      �� 5'      $%��Общая информация��6    $��16(�5(      $��	Helvetica)�5)      
�������$��Center=�5+      $��Center=�5,      5-      50      �� 53      $��Отработки:��6   $��Center=�54      $
��Left=�63   
�������$��17(�56      $��1��6&   59      &   System.Windows.Controls.DataGrid  5:      -   5          Name$	 otr��5;         ColumnHeaderHeight$ 27Q�5:      $��2��6/      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 5;   $      CanUserResizeColumns .  	   CanUserResizeRows	 .  
   CanUserReorderColumns
 .     CanUserSortColumns .     CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    SelectionMode$ Single=�5?         GridLinesVisibility$ 
Horizontal=�5@         HeadersVisibility$ Column=�68   
������ 5D      �� 5F      $��Субботники:��6   $��Center=�5G      $
��Left=�63   
�������$��17(�5I      $��1��6&   $��2��63   5L        5M      -   5        $	 sub��5N      $ 27Q�5M      $��2��6/   $��2��6<   $
 Auto 5N   $    .  	 .  
 .   .   .   .   . $ Single=�5R      $ 
Horizontal=�5S      $ Column=�68   
������ 5W      �� 5Z      $ 
Horizontal=�6   $��3��62   .+�5[      �� $��Всего:��6   $��Center=�5\      $
��Left=�63   
�������$��17(�5^      $��3��6&   �� 5`      -   5         ��Name$ countOtr��5`      $��10 0 0 0q�5a      $��Center=�5b      $��17(�67   
�������5c      5e      �� 5g      $ 
Horizontal=�6   $��3��63   $��2��6@   .+�5h      �� $��Всего:��6   $��Center=�5i      $
��Left=�63   
�������$��17(�5k      �� 5n      -   5        $ countSub��5n      $��10 0 0 0q�5o      $��Center=�5p      $��17(�67   
�������5q      5r      5v      5x      5y      