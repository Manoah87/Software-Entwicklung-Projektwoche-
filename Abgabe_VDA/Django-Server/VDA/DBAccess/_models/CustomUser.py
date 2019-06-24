from __future__ import unicode_literals

from django.db import models
from django.core.mail import send_mail
from django.contrib.auth.models import PermissionsMixin
from django.contrib.auth.base_user import AbstractBaseUser
from django.utils.translation import ugettext_lazy as _

from DBAccess.Managers import UserManager 

class UserInfo(AbstractBaseUser, PermissionsMixin): 
    UserName = models.CharField(_('Benutzername'), max_length=100, blank=True, unique=True)
    FirstName = models.CharField(_('Vorname'), max_length=100, blank=True)
    LastName = models.CharField(_('Nachname'), max_length=100, blank=True)
    email = models.EmailField(_('E-Mail'), unique=True)
    Enabled = models.BooleanField(_('active'), default=True)
    UserPrivilegeLevel = models.IntegerField(_('Privileg Level'), default = 0)
    DateCreated = models.DateTimeField(_('date joined'), auto_now_add=True)
    avatar = models.ImageField(upload_to='avatars/', null=True, blank=True)

    @property
    def UserInfoID(self):
        return self.id

    objects = UserManager()

    USERNAME_FIELD = 'UserName'
    REQUIRED_FIELDS = []

    class Meta:
        db_table = "User"
        verbose_name = _('user')
        verbose_name_plural = _('users')

    def get_full_name(self):
        '''
        Returns the first_name plus the last_name, with a space in between.
        '''
        full_name = '%s %s' % (self.first_name, self.last_name)
        return full_name.strip()

    def get_short_name(self):
        '''
        Returns the short name for the user.
        '''
        return self.first_name

    def email_user(self, subject, message, from_email=None, **kwargs):
        '''
        Sends an email to this User.
        '''
        send_mail(subject, message, from_email, [self.email], **kwargs)

    def get_username(self):
        return self.UserName