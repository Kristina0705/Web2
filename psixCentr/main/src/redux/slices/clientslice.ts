import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {User} from "../../models/UserModel";

export interface State {
	isAuth: boolean,
	user: User | null
}

const initialState: State = {
	isAuth: localStorage.getItem('user') !== null,
	user: localStorage.getItem('user') === null ? null : JSON.parse(localStorage.getItem('user')!) as User
}

const clientSlice = createSlice({
	name: 'Client',
	initialState: initialState as State,
	reducers: {
		registerSuccess: (state, action: PayloadAction<State>) => state = action.payload,
		loginSuccess: (state, action: PayloadAction<State>) => state = action.payload,
		logout: (state) => state = {isAuth: false, user: null},
		addedAvatar: (state, action: PayloadAction<User>) => {
			state.user = action.payload
		}
	}
});

export const clientReducer = clientSlice.reducer;
export const clientActions = clientSlice.actions;