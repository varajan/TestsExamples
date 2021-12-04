import requests

from Constants import Constants


class Users:
    @staticmethod
    def delete_all():
        requests.delete(Constants.URL + "/Register/DeleteAll", verify=False)

    @staticmethod
    def delete(login):
        data = {'login': login}
        requests.delete(Constants.URL + "/Register/Delete", data=data, verify=False)

    @staticmethod
    def create(login):
        data = {'login': login, 'password': Constants.PASSWORD, 'email': login + "@test.com"}
        Users.delete(login)
        requests.post(Constants.URL + "/Register/Register", data, verify=False)
