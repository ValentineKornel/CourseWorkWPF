   M S B A M L   `   `   `  ����B  >GlumHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:GlumHubGlumHub  5          GlumHub.UserPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:GlumHub     5         PageBackground  N� 
Background#  �   5   1    N� Title$ UserPage��5	       � DataContext 5        GlumHub.UserPageVM  5   
   6   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��50* �6   6-   �� 5      $
��700* �6   6.   �� 5      $	��50* �6   6-    � RowDefinitions 5   
   �� 5      $	��50* �6   $��50��6)   6:   �� 5      $
��112* �6   6,   .+�5   
   �� $��1��6    �� Orientation$ 
Horizontal=�6%   $��40, 40, 0, 0q�6>   .+�5      ��   BigCircularImageBorder#��[ 6   $��20, 20, 0, 0q�6E   $	��150��6[   $	��150��6h   $	��Top=�6t   ��5      �� 5       �� ImageSource 6!   �� *User.ProfileImage+  ByteArrayToImageConverter �� 	Converter# [ 5      �� 5      $	��700��6   $	��200��6%   $	��Top=�62   $��0, 20, 0, 0q�6J   .+�5      �� $
��auto��6   $��80��6*   $ 
Horizontal=�66   $
��Left=�6O   $��10, 0, 0, 0q�6j   .+�5       �� $
��auto��6   $��40��6)   $��24(�6X   ��65   �� *User.Firstname+6h   �� 5!      $
��auto��6   $��40��6)   $��24(�6Y   ��65   �� *User.Secondname+6i   �� 5"      -   5         ��Name$! subscribeUnsubscribeCommand��5"        DeleteUserButton#���  6h   $��12(�6�   $
��auto��6�   $��30��6�   $��20, 0, 20, 0q�6�   $y�Visible=�6�   ��6B   �� *DeleteUserCommand+6�   5$      �� 5%      $
��auto��6   $��80��6*   $
��Left=�66   $��10, 0, 0, 0q�6Q   .+�5&      �� $��35��6   $��18(�6(   ��66   �� *!User.MasterInfo.BusinessAddress+6l   �� 5'      $��35��6   $��18(�6(   ��66   �� *User.MasterInfo.Bio+6`   5(      5*      5+      �� 5,   
   $��1��6   $��1��6%   .+�5-      �� 
���   �$��1q�6)   $
��auto��6=   $��1��6J    � 5.      -   5        	  �Name$	 userPageForAdminFrame��5.      $	��690��63   5/      50      51      