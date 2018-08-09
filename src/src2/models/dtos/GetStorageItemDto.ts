export class GetStorageItemDto {
    public Created: Date = new Date();

    public Updated: Date = new Date();

    public Id: string = "";

    public Name: string = "";

    public BsonDocument: string = "";

    public Data: any;
}
