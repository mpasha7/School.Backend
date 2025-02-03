export interface MessageListVm {
    messages: MessageLookupDto[];
}

export interface MessageLookupDto {
    id: number;
    senderGuid: string;
    senderName: string;
    recipientGuid: string;

    theme: string;
    text: string;
    email: string | null;
    phone: string | null;
    isRead: boolean;

    answers: MessageLookupDto[] | null;
}