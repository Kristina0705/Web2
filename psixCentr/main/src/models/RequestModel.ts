export interface Answer{
	status: boolean,
	answer: any,
	error: number | null,
	errorText: string | null
}

export interface RegistrationModel {
	password: string,
	name: string,
	surname: string,
	email: string
}

export interface LoginModel {
	email: string,
	password: string
}