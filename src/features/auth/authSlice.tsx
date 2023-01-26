import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { IUser, ILogin } from "../../interfaces";

const initialState : IUser = {
  isFetching: false,
  isSuccess: false,
  isError: false,
  errorMessage: '',
  token: '',
  user: {
    refreshToken: '',
    email : '',
    username : ''
  }
}

export const authSlice = createSlice({
  name: 'token',
  initialState,
  reducers: {
    // logIn: (state, action) => {
    //   const { user, 
    //     refreshToken, 
    //     expirationDate, 
    //     accessToken } = action.payload;
    //   },
    //   logOut: (state, action) => {
        
    //   },
    //   refreshToken: (state, action) => {

      // }
    },
    // {
    // },
    extraReducers: (builder) => {
      builder.addCase(login.fulfilled, (state, action) => {
            state.isFetching = false;
            state.isSuccess = true;
            state.user.refreshToken = action.payload.user.refreshToken;
            state.user.email = action.payload.user.email;
            state.user.username = action.payload.user.name;
          }),
          builder.addCase(login.pending, (state : IUser) => {
            state.isFetching = true;
          }),
          builder.addCase(login.rejected, (state : IUser, payload ) => {
            state.isFetching = false;
            state.isError = true;
            // state.errorMessage = payload.error;

          })
      }
  })

export const login = createAsyncThunk(
  "user/login",
  async ( { email, password } : ILogin , thunkAPI) => {
    try {
      const res = await fetch("");
      return (await res.json());
    } catch(e){

      return thunkAPI.rejectWithValue("");
    }
  })

// export {login} as authSlice.actions;
export default authSlice.reducer;