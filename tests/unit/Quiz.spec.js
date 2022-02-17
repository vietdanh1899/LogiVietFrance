import Quiz from '../../src/components/Quiz.vue'
const { mount, createLocalVue } = require("@vue/test-utils")
import Buefy from 'buefy';

const localVue = createLocalVue();
localVue.use(Buefy);


describe("Quiz test function", () => {
    let wrapper = null;
    beforeEach(async () => {
        wrapper = mount(Quiz, {localVue});
    })

    it("UT001- should render all item", () => {
        expect(wrapper.findAll(".b-radio")).toHaveLength(3);
    })

    it("UT002- check right answer should show right", async () => {
        await wrapper.find("input[value='a']").trigger('click');
        expect(wrapper.find("input[name='answer'][value='a']").element.checked).toBeTruthy()
        expect(wrapper.find(".message").text()).toEqual("Correct");
    })

    it("UT003- check wrong answer should show wrong", async () => {
        await wrapper.find("input[value='b']").trigger('click');
        expect(wrapper.find("input[name='answer'][value='b']").element.checked).toBeTruthy()
        expect(wrapper.find(".message").text()).toEqual("Wrong");
    })
})