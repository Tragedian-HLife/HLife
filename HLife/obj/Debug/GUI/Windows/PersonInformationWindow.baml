   M S B A M L   `   `   `  ����@  <HLife, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HLifeHLife  5   	   #    HLife.PersonInformationWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:HLife     5   	   -   5          � Title$  PersonInformation��5   	   $	��487��6#   $	��490��60    � Left$
 1420��6<   
 � Top$	 500��6H    � ShowInTaskbar .  .��5	      � -   5         �Name$ grid_Primary��5	       � ColumnDefinitions 5
   
   �� 5      $
��155* �6   �� 5      $
��327* �6   .+�5   
   �� -   5         ��Name$ 	tabs_Info��5      $��1��6(   .@�5      �� -   5         ��Name$ 
tab_Status��5      $��Status��6*   .��5      � -   5        $ grid_Status��5      ���	#FFF3F3F3.+�5      �� $
��Auto=�6#   .��5      �� -   5         ��Name$ wrap_Status��5   %   $	��428��6:   $	��Top=�6G   $	��303��6_   $
��Left=�6k   5      5      5      �� 5      -   5        $ tab_Attributes��5      $��
Attributes��6.   .��5      � -   5        $ grid_Attributes��5      ���	#FFE5E5E5.+�5      �� .��5      �� -	   5        $ stack_Attributes��5   %   $	��428��6?   $	��303��6L   $
��Left=�6X   $	��Top=�6s   5      5      5      �� 5      -
   5        $ tab_Inventory��5      $��	Inventory��6-   .��5      � -   5        $ grid_Inventory��5      .+�5      � -   5        	 �Name$	 wrap_Inventory��5       $��10q�68   5       5!      5"      �� 5#   
   -   5        
 ��Name$
 
img_Avatar��5#      $��Stretch=�6$   $
��Auto��6B   $��10q�6P   $��Stretch=�6\   $
��Auto��6x   5%      5&      