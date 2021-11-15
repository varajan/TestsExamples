class Formatter:

    @staticmethod
    def format_date(date, date_format):
        if date_format == "dd/MM/yyyy":
            return date.strftime("%d/%m/%Y")
        if date_format == "dd-MM-yyyy":
            return date.strftime("%d-%m-%Y")
        if date_format == "MM/dd/yyyy":
            return date.strftime("%m/%d/%Y")
        if date_format == "MM dd yyyy":
            return date.strftime("%m %d %Y")

        raise ValueError("Invalid format: " + date_format)

    @staticmethod
    def format_number(number, number_format):
        result = "{:,.2f}".format(float(number))

        if number_format == "123.456.789,00":
            return result.replace(".", "x").replace(",", ".").replace("x", ",")
        if number_format == "123 456 789.00":
            return result.replace(",", " ")
        if number_format == "123 456 789,00":
            return result.replace(",", " ").replace(".", ",")

        return result
