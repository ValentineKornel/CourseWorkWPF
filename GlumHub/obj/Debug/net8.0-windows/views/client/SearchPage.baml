   M S B A M L   `   `   `  ����B  >GlumHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:GlumHubGlumHub  5          GlumHub.SearchPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:GlumHub     5         PageBackground  N� 
Background#  �   5   2    N� Title$ 
SearchPage��5	       � DataContext 5        GlumHub.SearchPageVM  5   
   6   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��160* �6   6.   �� 5      $
��100* �6   6.   .+�5   
   �� $��20,0,0,0q�6   .+�5      �� $
��auto��6   $
��auto��6&   $��0, 10, 0, 0q�64   .+�5      �� $��40��6   $	��730��6*   $��18(�66   $
��Left=�6D   . �5      /� .T�5      J� .*�6$   .*�5      �� �6"   �� *
SearchText+5      5      5      �� 5      $��40��6;   $��40��6F   $y�Hidden=�6R   $��Right=�6f   $��0, -40, 10, 0q�6�   ��6   �� *SearchCommand+6�   5      �� 5      $
��1000��6   $��0, 20, 0, 0q�6'   $
��auto��6<   .+�5      �� $��0q�6C   $
��auto��6W   ��6   �� *SearchedMasters+��5       �� 5!      .R�5"      �� $
��auto��5#   !   $
��auto��6.     SecondaryButtonStyle#��[ 6<   ��5"   %   �� *MasterPageRedirectCommand+��6S   �� *	master.Id+.��5$   "   �� $��1��6-    �� Orientation$ 
Horizontal=�6=   $	��730��6V   $	��150��6b   $��
0, 0, 0, 0q�6o   .+�5%   &   ��   BigCircularImageBorder#��[ 6-   $��20, 10, 0, 0q�6]   $	��120��6s   $	��120��6�   $	��Top=�6�   ��5&   *   �� 5'   .    �� ImageSource 69   �� *master.ProfileImage+  ByteArrayToImageConverter �� 	Converter# [ 5)   '   �� 5*   &   $
��auto��61   $
��auto��6>   $	��Top=�6L   $��0, 10, 0, 0q�6d   .+�5+   *   �� $
��auto��65   $
��auto��6B   $ 
Horizontal=�6P   $
��Left=�6i   $��10, 0, 0, 0q�6�   .+�5,   .   �� $
��auto��64   $��40��6A   $��24(�6r   ��6M   �� *master.Firstname+6�   �� 5-   .   $
��auto��64   $��40��6A   $��24(�6s   ��6M   �� *master.Secondname+6�   �� 5.   .   $��/files/subscribed.png��64   $��20��6S   $��20��6^   $��10, 0, 0, 0q�6j   y�6   �� *SubscribeTickVisibility+6�   5/   +   �� 50   *   $��30��60   $
��auto��6<   $��18(�6I   $��Oblique$�6{   $��10, 0, 10, 0q�6�   $��10, 0, 0, 0q�6�   ��6W   �� *master.Username+6�   �� 51   *   $��40��60   $��18(�6<   $��10, 0, 0, 0q�6�   ��6J   �� *#!master.MasterInfo.BusinessAddress+6�   52   '   53   #   54      55      57      5:      5;      �� 5<   
   $��1��6   $ 
Horizontal=�6%   .+�5=      �� 
���   �$��2q�6)   $��1��6=   $
��auto��6G   $��0, 60, 0, 0q�6U   6l   �� 5>      $
��auto��6   $��10, 40, 0, 0q�6&   .+�5?      ��   	CityLabel#���  6   $��18(�6>   $
��auto��6L   $��40��6Y   6g   �� 5@      $��40��6   $	��300��6*   $
��Left=�66   . �5A      /� .T�5B      J� .*�6$   .*�5C      �� �6"   �� *
SearchCity+5D      5E      5F      5G      5H      5J      5K      