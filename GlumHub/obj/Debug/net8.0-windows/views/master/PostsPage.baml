   M S B A M L   `   `   `  ����B  >GlumHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:GlumHubGlumHub  5          GlumHub.PostsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:GlumHub     5         PageBackground  N� 
Background#  �   5   1    N� Title$ 	PostsPage��5	       � DataContext 5        GlumHub.PostsPageVM  5   
   6   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��110* �6   6.   �� 5      $��1030* �6   6/   �� 5      $
��100* �6   6.    � RowDefinitions 5   
   �� 5      6   .+�5   
   ��   NewPostButton#���  6q   $
��auto��6�   $��30��6�   $	��Top=�6�   $��0, 10, 0, 0q�6�   ��6   �� *AddNewPostRedirectCommand+y�6?   �� *AddNewPostButtonVisibility+6�   �� 5   
   $��0q�61   $
��Left=�6E   $
��auto��6`   $��1��6m   ��6   �� *Posts+��5      �� 5      .R�5      � $	��900��6    5      ��5      �� 5       .R�5!      �� $	��210��6n   $	��210��6z   
������ $��
0, 0, 0, 0q�6�   ��6   �� *PostPageRedirectCommand+��6I   �� *	post.Id+.��5"      �� $	��210��6    $	��210��6,   ��69   �� *post.PostImage+6]   5#      5$      5&      5)      5*      