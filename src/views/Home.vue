<template>
  <div class="content">
    <div
      style="align-items: center; display: inline-flex; flex-direction: column"
    >
      <h4><b> Question: </b>{{ $options.myquestions.question }}</h4>
      <section>
        <b-field
          v-for="(answer, key) in $options.myquestions.answers"
          :key="key"
          style="text-align: left"
        >
          <b-radio
            v-model="radio"
            name="key"
            :native-value="key"
            :type="
              key === $options.myquestions.correctAnswer
                ? 'is-success'
                : 'is-danger'
            "
          >
            {{ answer }}
          </b-radio>
        </b-field>
      </section>
      <div v-show="radio" style="align-self: stretch">
        <b-message
          v-if="radio === $options.myquestions.correctAnswer"
          type="is-success"
        >
          Correct
        </b-message>
        <b-message v-else type="is-danger"> Wrong </b-message>
      </div>
    </div>
  </div>
</template>
<script>
import questions from "../../questions";

export default {
  name: "Home",
  data: function () {
    return {
      radio: "",
    };
  },
  myquestions: questions,
};
</script>