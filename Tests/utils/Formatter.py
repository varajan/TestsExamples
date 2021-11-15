class DateFormatter:

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
