   M S B A M L   `   `   `  ����B  >GlumHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:GlumHubGlumHub  5          GlumHub.MasterServicePage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:GlumHub     5         PageBackground  N� 
Background#  �   5   1    N� Title$ MsterServicePage��5	       � DataContext 5        GlumHub.MasterPageVM  5   
   6   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��110* �6   6.   �� 5      $��1030* �6   6/   �� 5      $
��100* �6   6.    � RowDefinitions 5   
   �� 5      6   .+�5   
   �� $��1��6   $
��auto��6%   .+�5      �� $��0q�6<   $
��auto��6P   ��6   �� *FreeBookings+��5      �� 5      .R�5      �� $	��800��6%   $
��auto��61   $��10, 10, 0, 10q�6?   .+�5      �� $��18(�6J   ��6$   �� *booking.Date_Time+6Z   �� 5      $��16(�6H   ��6$   �� *booking.Service+6X   �� 5      $
��Book��5   !   $
��auto��60   $��18(�6=   $��Right=�6K   $��0, -60, 0, 0q�6g   $��30��6}   $��20, 0, 20, 0q�6�   ��5   %   �� *BookFreeBookingCommand+��6P   �� *
booking.Id+5   �   5       5!      5#      5$      5&      5'      