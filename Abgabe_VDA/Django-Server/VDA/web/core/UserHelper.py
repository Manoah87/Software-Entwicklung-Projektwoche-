from django.contrib.auth import authenticate, login #authenticate, login
from django.http import HttpRequest
from DBAccess._models.CustomUser import UserInfo

class UserHelper(object):
    def AuthenticateUser(request : HttpRequest):
        if(request.user.is_anonymous):
            user = UserInfo.objects.filter(UserName='public')
            user.backend = 'django.contrib.auth.backends.ModelBackend'
         #   login(request, user)    

    #    user = authenticate(username=username, password=password)
    #    if user is not None:
    #    if user.is_active:
    #        login(request, user)
    #        # Redirect to a success page.
    #    else:
    #        # Return a 'disabled account' error message
    #else:
    #    # Return an 'invalid login' error message.

    def authenticate_custom(self, request, username=None, password=None):
        login_valid = (settings.ADMIN_LOGIN == username)
        pwd_valid = check_password(password, settings.ADMIN_PASSWORD)
        if login_valid and pwd_valid:
            try:
                user = User.objects.get(username=username)
            except User.DoesNotExist:
                # Create a new user. There's no need to set a password
                # because only the password from settings.py is checked.
                user = User(username=username)
                user.is_staff = True
                user.is_superuser = True
                user.save()
            return user
        return None
