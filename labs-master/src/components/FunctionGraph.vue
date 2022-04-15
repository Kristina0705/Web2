<template>
  <Line :chart-options="chartOptions" :chart-data="chartData" />
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { Line } from "vue-chartjs";
import { Chart, registerables } from "chart.js";
Chart.register(...registerables);

export default defineComponent({
  props: {
    function: { type: Function, default: null },
    x1: { type: Number, default: 0 },
    x2: { type: Number, default: 10 },
    dx: { type: Number, default: 1 },
    max: { type: Number, default: 10 },
    min: { type: Number, default: -10 },
    label: { type: String, default: "function" },
  },
  components: { Line },
  data() {
    const f: (x: number) => number =
      (this.function as unknown as (x: number) => number) || ((x: number) => x);
    const labels = [];
    const data = [];
    for (let i = 0; i <= (this.x2 - this.x1) / this.dx; i++) {
      const x = this.x1 + this.dx * i;
      labels.push(x);
      data.push(Math.max(Math.min(f(x), this.max), this.min));
    }
    return {
      chartData: {
        labels,
        datasets: [
          {
            label: this.label,
            data,
            borderColor: "rgba(75, 192, 192, 1)",
            fill: false,
          },
        ],
      },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false,
      },
    };
  },
});
</script>
