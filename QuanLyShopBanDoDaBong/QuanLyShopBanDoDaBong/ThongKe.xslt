<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<html>
			<head>
				<title>Báo Cáo Thống Kê</title>
				<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
				<style>
					body { font-family: 'Segoe UI', Arial, sans-serif; background-color: #f4f4f9; padding: 20px; }
					.container { width: 900px; margin: 0 auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 0 10px rgba(0,0,0,0.1); }
					h1 { text-align: center; color: #333; margin-bottom: 5px; }
					.date { text-align: center; color: #777; margin-bottom: 30px; font-style: italic; }

					/* Bảng dữ liệu */
					table { width: 100%; border-collapse: collapse; margin-top: 20px; }
					th { background-color: #007bff; color: white; padding: 10px; text-align: left; }
					td { padding: 10px; border-bottom: 1px solid #ddd; }
					tr:nth-child(even) { background-color: #f9f9f9; }

					/* Khung biểu đồ */
					.chart-box { width: 100%; margin-top: 40px; padding: 20px; border: 1px solid #eee; border-radius: 8px; }
				</style>
			</head>
			<body>
				<div class="container">
					<h1>BÁO CÁO CỬA HÀNG BÓNG ĐÁ</h1>
					<div class="date">
						Ngày xuất: <script>document.write(new Date().toLocaleDateString('vi-VN'));</script>
					</div>

					<div class="chart-box">
						<canvas id="myChart"></canvas>
					</div>

					<h3 style="margin-top:40px; color:#444;">Chi tiết số liệu:</h3>
					<table>
						<tr>
							<xsl:for-each select="NewDataSet/Table[1]/*">
								<th>
									<xsl:value-of select="name()"/>
								</th>
							</xsl:for-each>
						</tr>
						<xsl:for-each select="NewDataSet/Table">
							<tr>
								<xsl:for-each select="*">
									<td>
										<xsl:value-of select="."/>
									</td>
								</xsl:for-each>
							</tr>
						</xsl:for-each>
					</table>
				</div>

				<script>
					// Lấy dữ liệu từ XML để vẽ biểu đồ
					var labels = [];
					var dataValues = [];

					<xsl:for-each select="NewDataSet/Table">
						labels.push('<xsl:value-of select="*[1]"/>'); // Cột 1 làm Nhãn (Tên/Ngày)
						dataValues.push('<xsl:value-of select="*[last()]"/>'); // Cột cuối làm Giá trị (Số tiền/Số lượng)
					</xsl:for-each>

					// Cấu hình biểu đồ Chart.js
					var ctx = document.getElementById('myChart').getContext('2d');
					var myChart = new Chart(ctx, {
					type: 'bar', // Loại biểu đồ: 'bar' (cột), 'line' (đường), 'pie' (tròn)
					data: {
					labels: labels,
					datasets: [{
					label: 'Thống kê',
					data: dataValues,
					backgroundColor: 'rgba(54, 162, 235, 0.6)',
					borderColor: 'rgba(54, 162, 235, 1)',
					borderWidth: 1
					}]
					},
					options: {
					responsive: true,
					plugins: {
					title: { display: true, text: 'BIỂU ĐỒ TRỰC QUAN' }
					}
					}
					});
				</script>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>