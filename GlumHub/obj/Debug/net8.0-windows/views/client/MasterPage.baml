   M S B A M L   `   `   `  ����B  >GlumHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:GlumHubGlumHub  5          GlumHub.MasterPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:GlumHub     5         PageBackground  N� 
Background#  �   5   2    N� Title$ 
MasterPage��5	       � DataContext 5        GlumHub.MasterPageVM  5   
   6   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��50* �6   6-   �� 5      $
��700* �6   6.   �� 5      $	��50* �6   6-    � RowDefinitions 5   
   �� 5      $	��50* �6   $��50��6)   6:   �� 5      $
��112* �6   6,   .+�5   
   �� $��1��6    �� Orientation$ 
Horizontal=�6%   $��40, 40, 0, 0q�6>   .+�5      ��   BigCircularImageBorder#��[ 6   $��20, 20, 0, 0q�6E   $	��150��6[   $	��150��6h   $	��Top=�6t   ��5      �� 5       �� ImageSource 6!   �� *Master.ProfileImage+  ByteArrayToImageConverter �� 	Converter# [ 5      �� 5      $	��700��6   $	��200��6%   $	��Top=�62   $��0, 20, 0, 0q�6J   .+�5       �� $
��auto��6   $��80��6*   $ 
Horizontal=�66   $
��Left=�6O   $��10, 0, 0, 0q�6j   .+�5!      �� $
��auto��6   $��40��6)   $��24(�6Z   ��65   �� *Master.Firstname+6j   �� 5"      $
��auto��6   $��40��6)   $��24(�6[   ��65   �� *Master.Secondname+6k   �� 5#      -   5         ��Name$! subscribeUnsubscribeCommand��5#      $��12(�6�   $
��auto��6�   $��30��6�   $��20, 0, 20, 0q�6�   $y�Visible=�6�   ��6B   �� *SubscribeUnsubscribeCommand+��6r   �� *SubscriptionButtonContent+	 �� Mode$	 TwoWay=�6  5%      �� 5&      $
��auto��6   $��80��6*   $
��Left=�66   $��10, 0, 0, 0q�6Q   .+�5'      �� $��35��6   $��18(�6(   ��66   �� *#!Master.MasterInfo.BusinessAddress+6n   �� 5(      $��35��6   $��18(�6(   ��66   �� *Master.MasterInfo.Bio+6b   5)      5+      5,      �� 5-   
   $��1��6   $��1��6%   .+�5.      �� 
���   �$��1q�6)   $
��auto��6=   $��1��6J   �� 5/      $��50��6   $
��auto��6%   $��Center=�62   $ 
Horizontal=�6O   .+�50      �� $
��auto��6H   $��40��6U   $��18(�6a     ServiceButton#���  6o   $��10, 0, 10, 0q�6�   $��20, 0, 20, 0q�6�     SecondaryButtonStyle#��[ 6�   ��6   �� *ServicePageRedirectCommand+6�   �� 51      $
��auto��6F   $��40��6S   $��18(�6_     PoststButton#���  6m   $��10, 0, 10, 0q�6�   $��20, 0, 20, 0q�6�   #��[ 6�   ��6   �� *PostsPageRedirectCommand+6�   52       � 53      -   5        
  �Name$
 masterPageForClientFrame��53      $	��690��66   56      57      58      