<div class="row">
	<div class="col-lg-12">
		<h1 class="page-header">Olá, Administrador!</h1>

		{{--<p>--}}
		{{--This is simple demo application. It contains only 3 entity types:--}}
		{{--<ul>--}}
		{{--<li><code>Contact</code> &mdash; contains contact info, "belongs-to" relation with countries,--}}
		{{--"many-to-many" relation with companies--}}
		{{--</li>--}}
		{{--<li><code>Company</code> &mdash; contains title, address and phone, "many-to-many" relation with contacts--}}
		{{--</li>--}}
		{{--<li><code>Country</code> &mdash; contains title, "has-many" relation with contacts</li>--}}
		{{--</ul>--}}
		{{--</p>--}}
		{{--<p>Source of the demo is available on <a href="https://github.com/sleeping-owl/admin_demo">GitHub</a>.</p>--}}

		{{--<p>Source of the SleepingOwl Admin module is also available on <a href="https://github.com/sleeping-owl/admin">GitHub</a>.--}}
		{{--</p>--}}

		{{--<p>For details see <a href="http://sleeping-owl.github.io">SleepingOwl Admin documentation</a>.</p>--}}
	</div>
</div>

<div class="row">
	<h2>Dashboard</h2>
</div>

<div class="row">
	{{-- FLAGGED POSTS --}}
	<div class="col-lg-3 col-md-6">
		@if($reportedEntriesCount > 0)
			<div class="panel panel-red">
		@else
			<div class="panel panel-green">
		@endif
			<div class="panel-heading">
				<div class="row">
					<div class="col-xs-3">
						<i class="fa fa-flag-o fa-fw fa-5x"></i>
					</div>
					<div class="col-xs-9 text-right">
						<div class="huge">{{ $reportedEntriesCount }}</div>
						<div>Entradas Reportadas</div>
					</div>
				</div>
			</div>

			<a href="{{ Admin::instance()->router->routeToModel('entradas', ['reportadas' => 'yes']) }}">
				<div class="panel-footer">
					<span class="pull-left">Ver Entradas</span>
					<span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>

					<div class="clearfix"></div>
				</div>
			</a>
		</div>
	</div>

	{{-- FLAGGED COMMENTS --}}
	<div class="col-lg-3 col-md-6">
		@if($reportedCommentsCount > 0)
			<div class="panel panel-red">
		@else
			<div class="panel panel-green">
		@endif
			<div class="panel-heading">
				<div class="row">
					<div class="col-xs-3">
						<i class="fa fa-flag-o fa-fw fa-5x"></i>
					</div>
					<div class="col-xs-9 text-right">
						<div class="huge">{{ $reportedCommentsCount }}</div>
						<div>Comentários Reportados</div>
					</div>
				</div>
			</div>

			<a href="{{ Admin::instance()->router->routeToModel('comentarios', ['reportados' => 'yes']) }}">
				<div class="panel-footer">
					<span class="pull-left">Ver Comentários</span>
					<span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>

					<div class="clearfix"></div>
				</div>
			</a>
		</div>
	</div>

	{{-- NEWS --}}
	<div class="col-lg-3 col-md-6">
		<div class="panel panel-primary">
			<div class="panel-heading">
				<div class="row">
					<div class="col-xs-3">
						<i class="fa fa-newspaper-o fa-fw fa-5x"></i>
					</div>
					<div class="col-xs-9 text-right">
						<div class="huge">{{ $newsCount }}</div>
						<div>Notícias</div>
					</div>
				</div>
			</div>

			<a href="{{ Admin::instance()->router->routeToModel('entradas/create') }}">
				<div class="panel-footer">
					<span class="pull-left"><strong>Criar Notícia</strong></span>
					<span class="pull-right"><i class="fa fa-plus-circle"></i></span>

					<div class="clearfix"></div>
				</div>
			</a>

			<a href="{{ Admin::instance()->router->routeToModel('entradas', ['news' => 'yes']) }}">
				<div class="panel-footer">
					<span class="pull-left">Ver Notícias</span>
					<span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>

					<div class="clearfix"></div>
				</div>
			</a>
		</div>
	</div>

	{{-- SURVEYS --}}
	<div class="col-lg-3 col-md-6">
		<div class="panel panel-primary">
			<div class="panel-heading">
				<div class="row">
					<div class="col-xs-3">
						<i class="fa fa-bar-chart fa-fw fa-5x"></i>
					</div>
					<div class="col-xs-9 text-right">
						<div class="huge">{{ $surveysCount }}</div>
						<div>Inquéritos</div>
					</div>
				</div>
			</div>

			<a href="/admin/exportSurveysToCSV">
				<div class="panel-footer">
					<span class="pull-left">Exportar para .csv</span>
					<span class="pull-right"><i class="fa fa-file-excel-o"></i></span>

					<div class="clearfix"></div>
				</div>
			</a>
		</div>
	</div>
</div>
