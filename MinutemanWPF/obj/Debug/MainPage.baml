   M S B A M L   `   `   `  ����  MinutemanWPF    Minuteman.MainPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      !localusing:Minuteman     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � 	Resources  5
         LandingBackground �� 	UriSource 
 MImage  MinutemanLogo  appLogo  profile1Icon  settingsIcon  
iconCircle  meetingsIcon  TimelineViewButton 	 ContextualViewButton 
 FilterButton  MeetingActive  MeetingInactive  ActionActive  ActionInactive  UpdateActive  UpdateInactive  RotateImage �� 
TargetName �� TargetProperty	 W� To g� Duration g� RepeatBehavior  GetRidOfSplash��%�  &	        &	 <     &	 v     &	 �     &	 �     &	     &	 Q    &	 �    &	 �    &		     &	
 L    &	 �    &	 �    &	     &	 ?    &	 z    &	 �    &	 �    &	 �    �� 5   
   $( "./Assets/Background/background.png?�60   �� 5   
   $&  ./Assets/Logos/rotate_circle.png?�6%   �� 5   
   $ ./Assets/Logos/name.png?�6,   �� 5   
   $! ./Assets/Logos/app_logo.png?�6&   �� 5   
   $( "./Assets/Profiles/app_profile1.png?�6+   �� 5   
   $% ./Assets/Icons/app_settings.png?�6+   �� 5   
   $' !./Assets/Profiles/icon_circle.png?�6)   �� 5   
   $( "./Assets/Icons/app_meetingicon.png?�6+   �� 5   
   $, &./Assets/Icons/app_timeline_active.png?�61   �� 5   
   $0 *./Assets/Icons/app_contextual_inactive.png?�63   �� 5   
   $, &./Assets/Icons/app_filter_inactive.png?�6+   �� 5   
   $( "./Assets/Icons/app_meetingicon.png?�6,   �� 5   
   $( "./Assets/Icons/app_meetingicon.png?�6.   �� 5   
   $' !./Assets/Icons/app_actionicon.png?�6+   �� 5   
   $' !./Assets/Icons/app_actionicon.png?�6-   �� 5   
   $' !./Assets/Icons/app_updateicon.png?�6+   �� 5    
   $' !./Assets/Icons/app_updateicon.png?�6-   �� 5"   
   .��5#      W� $ rotateImage��6   $5 /(Image.RenderTransform).(RotateTransform.Angle)�6B   $	 360X�6�   $ 0:0:3D�6�   $ Forever��6�   5$      �� 5&   
   .��5'      W� $ splashScreen��6   $3 -(Grid.RenderTransform).(TranslateTransform.X)�5(      $
 3000X�5)      $ 0:0:0.2D�6(   5*      5F      .��5H      � .+�5I   
   �� #��[  6   $��UniformToFill=�6<   � 5K   
   -   5         �Name$ splashScreen��5K       K� ZIndex$
 9999��6%   z�5L      T� 5M      .+�5O      �� #��[  6   $��UniformToFill=�6@   � 5P      $��Center=�6   $��Center=�60   .+�5Q      �� -   5        	 ��Name$	 rotateImage��5Q      $	��100��6-   #��[ 6:   $
��Left=�6[   
 J� RenderTransformOrigin$
 0.5, 0.5&�6v   z�5R      �� 5S      6,   5U      �� 5V      $	��100��6   #��[ 6%   $
��Left=�6M   5W      5X      � 5Z   
   -   5        $ mainContainer��5Z       � RowDefinitions 5[      �� 5\      $��80 �6    �� 5]      $��* �6    .+�5_      � -   5        $ topBar��5_      $��0��6#   $��50��60   $��Center=�6<   .+�5`      �� #��[ 6   $��Center=�6:   $
��Left=�5a      $��50,0,0,0q�63   �� 5b      $��Right=�6   $��Center=�69    �� Orientation$ 
Horizontal=�6T   .+�5c      �� $��0��6    $��Friday, June 24��6-   $��Calibri)�6D   $��Center=�6Y   
�������$��20(�5d   3   $��2��5e      $��0,0,20,0q�6,   �� 5f      -   5        #��[ 5f      $��35��6D   $��Center=�6P   $��0,0,50,0q�5h      5i      5j      � 5k      $��1��6   $��	50,0,50,0q�6     5l      �� 5m      $��55 �6$   �� 5n      $��* �6$    � ColumnDefinitions 5p      �� 5q      $��* �6'   �� 5r      $��* �6'   �� 5s      $��* �6'   .+�5u      �� $��Meeting Title��6   $��Center=�6/   $��35��6J   $��0��6V   $��0��6f   $��2��6s   $��	0,10,0,10q�6�   .��6�   5w      �� 5y      $ 
Horizontal=�6   $��Right=�66   $��4��6R   .+�5z      �� $��30��6   $��0,0,10,0q�6(   #��[
 6:   5{      �� 5}      -   5         ��Name$ MinutesView��5~      ���	#22111111$��1��5}   2   $��4��6?   $��0,0,0,5q�6S   $��Stretch=�5~   ]   ��60   �� *	Minutes+ �� Mode$ TwoWay=���5      �� *ActualWidth+ �� ElementName$ MainContainer����5�      �� 5�       �� 
TargetType" ��6    .�5�      ��  �� Property � �� �� Value$ Stretch=�6K   6]   5�      ��5�      �� 5�      .R�5�      � $��0,3,0,3q�6#    5�   "   �� 5�   &   $	��100 �67   �� 5�   &   $	��100 �67   �� 5�   &   $��* �67   .+�5�   "   � $��0��6(   .+�5�   &   �� $��55��6\   $��Center=�6h   $��0,0,0,10q�6�   ��6,   �� *ProfileImageURI+$ OneWay=�5�   '   �� 5�   &   $��0,0,0,10q�6,   #��[ 6>   $��80��6c   $��Center=�6o   �� 5�   &   $��Calibri)�61   
�������$��CharacterEllipsis=�6Y   $��Center=�5�   1   $��Bottom=�6N   ��5�   1   �� *ProfileName+$ OneWay=�5�   #   �� 5�   "   $��1��6-   .+�5�   &   �� $��65��6N   $��15q�6Z   ��6,   �� *ActionImageURL+5�   '   �� 5�   &   $��Center=�60   $��CharacterEllipsis=�5�   0   $��Calibri)�5�   0   
���������5�   M   �� *ActionMetadata+5�   E   5�   #   �� 5�   "   $��Calibri)�5�   ,   
�������$��20q�6T   $��20(�6`   $��2��6n   ��6~   �� *Description+5�      5�      5�      5�      5�      5�      5�      