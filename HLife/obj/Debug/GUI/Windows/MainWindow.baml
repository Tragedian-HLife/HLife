   M S B A M L   `   `   `  ����@  <HLife, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HLifeHLife  5   	   7 clr-namespace:HLife.GUI.ControlsHLife.GUI.Controls  5   	       HLife.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:HLife     5   	   5Controls clr-namespace:HLife.GUI.Controls     5   	   -   5          �Name$  
GameWindow��5   ;    � Title$ HLife��5	   	   $	��850��6   $
��1420��6$    � WindowStartupLocation$ Manual=�61    � Icon$$ /HLife;component/Misc/logo.ico��6o   $
��Left=�6�   $	��Top=�6�    � Left$ 0��6�   
 � Top$ 0��6�   .��5
      � -   5         �Name$ 	grid_Main��5
      .+�5   
   �� -   5         ��Name$ tabs_Location��5      $��0,25,0,0q�6,   $
��Left=�6>   $	��351��6Y   .@�5      �� -   5         ��Name$ tab_Info��5      $
��Info��6(   .��5      � ���	#FFE5E5E56   5      �� 5      -   5        $ tab_Occupants��5      $��	Occupants��6-   .��5      � -   5        $ grid_Occupants��5      ���	#FFE5E5E5.+�5      &    HLife.GUI.Controls.PagingControl  -   5        	  Name$	 
Pagination��5   -   $��Bottom=�6A   $
��Auto��6\   �� 5      $��0,0,0,30q�6#   .��5      � -   5        
 �Name$
 wrap_Occupants��5   $   $��Center=�6<   $	��747��6Y   $��Visible=�6f   $��Center=�6�   $	��324��6�   5      5      5      �� 5      -	   5        $ tab_Inventory��5      $��	Inventory��6-   .��5      � -
   5        $ grid_Inventory��5      $��0,0,0,3q�6/   .+�5      � -   5        $
 wrap_Inventory��5       $��10q�68   5      5      5      o� 5   
   -   5         o�Name$ menu_Toolbar��5      $��20��6%   $	��Top=�61   ���	#FFF1F1F1.@�5      m� -   5         m�Name$ menuItem_File��5      $
��File��6.   .@�5      �� $
��TEST��6   5       m� 5!      -   5        $ menuItem_Edit��5!      $
��Edit��6.   .@�5"      �� $
��TEST��6   5#      5$      �� 5%   
   -   5         ��Name$ lbl_Date��5%      $��January 1, 12:00:00��6"   $	��Top=�6@   $��20��6X   $��Right=�6d   $	��193��6�   $��Right=�6�   $
��10,0q�6�   $��16(�6�   $��Source Sans Pro)�6�   � 5&   
   -   5        $ 	grid_View��5&      $��
356,25,0,0q�6"   .+�5'      �� -   5         ��Name$ img_LocationBackground��5'      64   5(      5)      5*      