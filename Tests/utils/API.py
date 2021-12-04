import requests

from Constants import Constants


class Users:
    @staticmethod
    def delete_all():
        requests.delete(Constants.URL + "/api/Register/DeleteAll", verify=False)

    @staticmethod
    def delete(login):
        data = {'login': login}
        requests.delete(Constants.URL + "/api/Register/Delete", json=data, verify=False)

    @staticmethod
    def create(login):
        data = {'login': login, 'password': Constants.PASSWORD, 'password2': Constants.PASSWORD, 'email': login + "@test.com"}
        Users.delete(login)
        requests.post(Constants.URL + "/api/Register", json=data, verify=False)
        x = 1