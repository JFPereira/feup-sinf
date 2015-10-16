<?php namespace App\Http\Controllers;

class AdminController extends Controller
{

	public function index()
	{
		$newsCount = 5;
		$reportedEntriesCount = 6;
		$reportedCommentsCount = 1;
		$surveysCount = 3;

		$data = compact('reportedEntriesCount', 'reportedCommentsCount', 'newsCount', 'surveysCount');

		return view('admin.index', $data);
	}

}
