import { NPSTemplatePage } from './app.po';

describe('NPS App', function() {
  let page: NPSTemplatePage;

  beforeEach(() => {
    page = new NPSTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
