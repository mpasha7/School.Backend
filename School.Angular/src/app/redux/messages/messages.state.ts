import { MessageLookupDto } from "../../core/models/message.model";


export interface MessagesState {
    messageList: MessageLookupDto[];
    loading: boolean;
    error: any;
}

export const initialMessagesState: MessagesState = {
    messageList: [],
    loading: false,
    error: null
}