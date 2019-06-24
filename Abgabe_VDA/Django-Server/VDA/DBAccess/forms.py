from django import forms
from django.contrib.auth.forms import UserCreationForm, UserChangeForm
from DBAccess._models.CustomUser import UserInfo

# Forms werden verwendet damit der Nutzer der Website etwas eingeben kann. Also mehr als nur lesen.

class CustomUserCreationForm(UserCreationForm):

    class Meta(UserCreationForm):
        model = UserInfo
        fields = ('username', 'email')

class CustomUserChangeForm(UserChangeForm):

    class Meta:
        model = UserInfo
        fields = UserChangeForm.Meta.fields
