import { GiftList } from './list-Gifts';
import { expect } from 'chai';
import 'mocha';
import { IGiftClient, GiftInput, Gift } from './secret-santa-api.client';


// Mock Object
class MockGiftClient implements IGiftClient {
    async getAll(): Promise<Gift[]> {
        return [
            new Gift({
                id: 999,
                title: 'Aladdin',
                description: 'The animated one, which is the only one that should exist.',
                url: 'https://www.aladdin.com/',
                userId: 3
            })
        ];
    }
    get(_id: number): Promise<Gift> {
        throw new Error('Method not implemented.');
    }
    put(_id: number, _value: GiftInput): Promise<Gift> {
        throw new Error('Method not implemented.');
    }
    post(_entity: GiftInput): Promise<Gift> {
        throw new Error('Method not implemented.');
    }
    delete(_id: number): Promise<void> {
        throw new Error('Method not implemented.');
    }
}


// Unit Tests
describe('getAllGifts', () => {
    it('should return one gift, being id of 42, and userId of 3', async () => {
        const gifts = await new GiftList(new MockGiftClient()).getAllGifts();
        expect(gifts.length).to.equal(1);
        expect(gifts[0].id).to.equal(999);
        expect(gifts[0].userId).to.equal(3);
    });
});

