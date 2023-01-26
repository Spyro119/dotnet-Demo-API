export interface IUser {
  isFetching: boolean;
  isSuccess: boolean;
  isError: boolean,
  errorMessage: string,
  token: string,
  user : {
    refreshToken : string,
    email : string,
    username : string
  }
}