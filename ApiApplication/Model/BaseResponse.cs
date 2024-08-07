namespace ApiApplication.Model
{
    public class BaseResponse
    {
        public string Message { get; set; }

        public int Code { get; set; }

        public void setSuccessCode()
        {
            this.Message = "Success";
            this.Code = 1;
        }

        public void setErrorCode(string errorMessage = "Something when wrong")
        {
            this.Message = errorMessage;
            this.Code = -1;
        }
    }
}
