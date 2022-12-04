import { User } from "./UserModel"

export interface Appeal {
	id: number
	typeappeal: string,
	user: User,
	text: string,
    isanswered:boolean,
    date:string,
    time:string,
}
export interface PostAppeal{
	typeappeal: string,
	text: string,
}