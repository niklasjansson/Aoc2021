﻿using MoreLinq;

var inputText =
@"--- scanner 0 ---
-809,-750,623
-853,-746,517
-136,-29,-84
318,-839,681
-474,-873,-609
727,841,-615
-464,-774,-678
-413,613,-400
660,790,-521
407,-813,737
809,365,495
336,-500,-487
306,-520,-581
-413,723,-516
9,72,-144
-638,485,266
-394,-871,-704
-579,631,334
418,-893,635
804,470,572
-365,640,-524
812,489,534
326,-543,-425
729,809,-486
-885,-695,487
-599,423,286

--- scanner 1 ---
449,611,-798
802,-370,-638
-724,508,707
-657,-438,613
559,-402,500
-617,-418,686
-718,895,-439
812,-468,-646
-528,-655,-312
-608,-540,-348
75,5,6
-760,512,753
574,-414,383
443,386,601
873,-483,-709
551,405,535
572,-563,422
440,664,-754
569,587,-716
-575,534,792
503,454,661
34,166,145
-688,-657,-293
-710,909,-311
-698,-362,567
-625,898,-447

--- scanner 2 ---
704,-944,-857
-733,515,415
-399,549,-834
502,761,-631
-364,654,-893
493,712,852
-402,629,-706
-655,439,375
664,769,-541
-534,-550,739
-698,-788,-816
-624,-569,721
-890,-797,-866
635,673,805
-545,534,401
559,633,-556
-563,-592,679
727,-804,-871
635,729,716
-42,-131,-38
508,-589,781
-683,-790,-928
782,-830,-890
489,-655,697
396,-551,798

--- scanner 3 ---
783,-601,626
-580,551,636
-925,-529,-667
665,-549,-523
-8,82,105
-579,541,761
800,-660,653
628,-439,-487
-809,-530,-545
620,418,774
-823,732,-387
-891,566,-359
-467,532,590
-521,-336,520
-525,-354,546
-115,-29,-4
-628,-367,385
-838,694,-404
549,540,-421
-806,-581,-754
558,502,-429
519,601,-521
821,-634,785
747,421,800
681,440,704
677,-467,-496

--- scanner 4 ---
705,-267,639
-584,827,-496
-33,55,-59
-618,-265,557
-647,501,615
674,-308,727
-684,608,591
413,450,680
-609,536,473
572,-670,-545
-708,-240,-869
427,393,-730
656,-688,-550
387,431,735
-748,-323,-973
-640,-253,402
-686,750,-611
420,455,-937
273,464,657
680,-254,719
-623,-332,592
429,505,-745
-744,-321,-744
-728,866,-558
426,-699,-584

--- scanner 5 ---
834,879,-701
-848,747,643
-370,-566,861
-737,713,-408
-657,-360,-496
-660,753,-420
-623,-396,-596
-62,62,50
781,530,341
424,-638,569
596,-570,591
-828,809,682
-485,-675,879
-621,-302,-404
668,-636,532
412,-838,-825
803,784,-712
-780,601,702
777,606,378
-797,708,-315
-569,-598,914
835,547,375
474,-747,-689
416,-782,-858
806,776,-610

--- scanner 6 ---
322,287,631
-662,-884,857
-141,-142,14
-513,491,-585
4,-3,106
-840,700,719
278,296,578
-814,-882,-303
637,-871,862
367,720,-516
765,-863,876
438,649,-403
-875,768,533
-397,532,-644
-764,774,672
360,416,554
430,659,-501
629,-778,865
-711,-871,711
-706,-816,-332
764,-681,-688
-673,-893,928
-791,-662,-272
728,-761,-573
783,-841,-690
-575,526,-648

--- scanner 7 ---
723,683,-372
-785,446,-893
-649,-583,-467
-501,-580,-465
-711,424,-880
-580,-524,-629
-719,487,403
-421,-436,791
-793,534,479
422,-780,737
532,612,-369
852,495,532
-405,-483,800
865,349,581
918,-625,-478
882,-482,-512
124,62,-55
-734,377,-773
874,278,570
16,-108,68
546,747,-406
-787,673,368
-417,-605,770
884,-514,-475
523,-676,644
439,-742,625

--- scanner 8 ---
-858,-680,-691
414,-517,587
625,667,442
-545,771,-555
-851,709,576
-602,685,-623
-722,-681,836
-2,154,-76
587,676,434
727,-677,-463
-515,733,-581
487,-488,566
-157,51,27
706,478,-787
526,706,534
637,-496,561
803,-740,-352
-823,-586,-854
-651,-625,786
-796,737,677
-513,-707,829
-795,780,485
687,-723,-404
709,605,-761
-767,-606,-703
687,544,-691

--- scanner 9 ---
109,7,73
-400,-440,-543
467,669,632
-30,-84,2
493,638,678
772,315,-497
678,-864,-522
486,-786,649
475,-708,755
-403,-430,-509
-538,-912,807
-513,341,481
-541,-861,616
451,712,698
-497,-958,636
413,-724,763
-641,288,436
-475,356,464
-675,712,-513
-666,600,-684
604,-729,-609
-494,-409,-454
-656,599,-559
796,438,-442
669,-761,-487
811,412,-602

--- scanner 10 ---
-518,-468,-442
-480,-360,707
356,-654,-380
469,774,401
739,538,-422
-696,447,-532
613,836,440
-813,392,-452
-846,720,431
-594,-514,-483
-630,-389,674
352,-608,-416
567,-534,637
-574,-587,-523
378,-693,-565
626,-440,604
669,-421,699
-810,929,405
-799,798,380
742,546,-515
-589,-305,834
753,410,-430
-176,104,-105
-6,-40,-35
511,721,517
-811,411,-612

--- scanner 11 ---
-301,-625,-354
-705,-484,764
-407,439,376
866,-341,-713
-767,430,-787
154,16,27
879,-346,586
436,783,430
-779,-531,688
773,-338,-705
537,679,523
866,-492,635
453,745,542
513,765,-590
-421,-649,-436
858,-327,-841
-391,486,367
-684,575,-767
-292,-572,-398
-403,608,397
-7,54,-106
-676,473,-882
495,784,-500
555,742,-529
-773,-670,799
888,-275,597

--- scanner 12 ---
575,709,-663
-520,-669,720
-582,759,436
-540,-332,-636
475,770,-693
655,-390,397
458,616,694
-465,-693,597
-746,889,-891
483,808,-702
413,744,747
-514,-362,-598
609,-364,366
-536,778,323
675,-348,323
441,578,679
-73,49,-59
-707,769,435
-582,-419,-684
-720,892,-764
617,-668,-660
-650,870,-825
-625,-708,623
526,-612,-589
718,-613,-632
46,185,7

--- scanner 13 ---
-440,691,-410
-953,891,363
-918,832,468
646,501,-442
381,-841,-740
576,447,426
-648,-353,-584
392,-640,312
-158,89,-157
-662,-377,578
462,-701,334
-772,-403,483
-443,688,-473
-584,-417,502
-915,704,388
781,608,-473
513,-820,-766
-391,673,-502
366,-767,315
540,-803,-707
530,560,445
620,505,-460
-572,-450,-527
-664,-344,-563
-60,23,14
405,559,435

--- scanner 14 ---
-799,642,-482
-610,668,689
409,462,776
501,694,-676
-736,-387,-705
623,-522,506
-698,670,-427
-607,599,-483
-192,168,-21
459,646,-763
439,-710,-426
-605,729,810
-486,-788,585
-690,-356,-731
-87,59,88
-470,-707,680
370,647,754
-592,810,815
384,-587,-402
433,608,-733
-453,-771,774
-756,-313,-747
544,-613,-387
529,-555,565
642,-617,463
455,498,780

--- scanner 15 ---
-45,111,125
849,932,816
589,-682,-407
-634,-519,-444
814,873,711
-702,-433,-387
480,-376,677
-676,-471,-357
-591,788,-733
-696,885,-694
530,967,-628
50,14,15
-496,-599,867
811,745,791
-566,-660,845
558,-336,601
-634,601,734
515,855,-626
-680,488,753
679,-700,-388
-751,762,-718
323,905,-635
668,-362,696
650,-640,-430
-509,600,744
-470,-726,864

--- scanner 16 ---
736,391,727
765,-597,557
735,-417,-729
28,87,-12
-560,-323,-329
740,354,803
-838,-302,625
681,852,-531
717,746,-623
-668,409,-594
-869,695,524
762,-699,549
794,388,786
-701,-407,580
-137,-21,125
-532,485,-649
793,-688,454
-655,-351,-286
-821,715,732
-638,441,-645
-778,-395,620
858,-485,-708
-889,656,586
660,843,-547
-703,-289,-413
810,-352,-693

--- scanner 17 ---
-488,691,763
-536,649,676
143,145,-109
950,-515,492
-354,-323,-379
775,508,-463
477,-687,-663
888,-308,485
-346,-451,-393
868,711,426
-489,780,-754
771,606,387
-439,-459,-390
680,527,-506
516,-749,-519
-450,717,733
-699,-574,713
-717,-664,732
838,-500,494
713,621,-565
-304,741,-776
745,766,402
-657,-635,690
593,-634,-621
-25,-44,-20
-340,695,-780

--- scanner 18 ---
858,-616,-298
855,-557,-356
-609,-269,-672
-841,412,515
-56,18,86
403,717,-432
-504,-511,536
-446,-227,-659
-623,974,-350
768,-587,440
-638,922,-321
467,781,654
500,793,-375
453,593,635
904,-445,457
892,-633,-352
105,134,162
-782,447,620
-620,-289,-646
-641,925,-258
-654,450,540
-489,-616,400
427,703,730
394,701,-411
857,-587,496
-579,-454,408

--- scanner 19 ---
-669,609,672
162,-147,-19
517,-725,570
-382,264,-818
686,-844,566
455,603,-473
602,657,-547
-601,-642,-787
759,-641,-815
898,-550,-766
47,-17,-148
623,-659,591
-571,617,682
-384,283,-658
-637,-529,429
-625,-412,444
-677,-596,-828
-556,680,743
461,732,-630
-476,-451,442
905,-687,-713
-499,319,-738
-621,-715,-747
804,653,371
761,480,437
693,560,351

--- scanner 20 ---
-772,521,434
-660,559,451
594,777,-568
703,-251,553
-826,865,-409
-700,-733,-772
-692,-569,-660
440,787,896
-835,818,-393
-675,-742,380
-679,625,535
-533,-703,427
-626,-597,-724
-533,-759,516
649,-388,561
-805,696,-405
443,676,930
580,-420,-503
730,-395,-594
631,-452,-584
694,-436,464
432,718,-637
430,721,-489
27,35,120
477,819,894

--- scanner 21 ---
637,-453,823
495,926,-740
-380,775,587
-572,-626,-694
742,570,865
-509,485,-368
-611,600,-445
-534,-565,494
-609,-602,-587
-534,-484,-659
723,660,847
741,-360,924
-383,783,494
700,599,805
703,-472,907
462,-746,-764
504,734,-690
-388,-516,481
-653,547,-329
-369,911,502
-317,-527,472
348,-668,-731
502,719,-802
488,-691,-745
-1,70,63

--- scanner 22 ---
355,509,452
-378,717,-376
-462,632,-339
-522,-796,-510
678,-666,731
478,454,428
543,693,-480
-746,-637,738
43,-95,68
-392,609,-533
-668,-743,749
-721,499,555
448,712,-437
-536,-779,-580
-560,-882,-592
-45,-8,-63
752,-686,773
-689,621,620
709,-620,757
294,-383,-748
-684,-751,811
510,599,-539
358,-400,-654
452,614,413
-824,534,546
277,-416,-559

--- scanner 23 ---
-599,295,-722
601,681,-857
383,788,454
-591,298,-815
577,-828,-711
-714,-808,-462
-463,284,-807
278,-550,646
-422,-516,727
-950,582,352
640,659,-672
-868,606,472
-775,-785,-504
509,-771,-625
509,739,406
429,-606,666
-182,-124,48
-804,-827,-444
-537,-618,735
-112,-17,-80
-517,-473,818
551,-858,-673
555,685,-821
449,807,346
441,-515,740
-906,529,343

--- scanner 24 ---
33,110,-60
642,719,-823
-679,-685,-841
693,689,-791
595,-609,-389
671,-666,-486
-684,872,-517
-476,920,-537
-738,-743,-844
-755,-689,-878
800,-720,652
-418,711,871
-538,940,-576
-391,-369,623
-411,-395,634
840,571,480
579,-630,-380
-349,-382,460
-523,634,866
-121,35,74
852,-691,637
760,469,469
803,-783,655
786,525,380
697,583,-747
-542,672,874";

//var inputText =
//@"--- scanner 0 ---
//404,-588,-901
//528,-643,409
//-838,591,734
//390,-675,-793
//-537,-823,-458
//-485,-357,347
//-345,-311,381
//-661,-816,-575
//-876,649,763
//-618,-824,-621
//553,345,-567
//474,580,667
//-447,-329,318
//-584,868,-557
//544,-627,-890
//564,392,-477
//455,729,728
//-892,524,684
//-689,845,-530
//423,-701,434
//7,-33,-71
//630,319,-379
//443,580,662
//-789,900,-551
//459,-707,401

//--- scanner 1 ---
//686,422,578
//605,423,415
//515,917,-361
//-336,658,858
//95,138,22
//-476,619,847
//-340,-569,-846
//567,-361,727
//-460,603,-452
//669,-402,600
//729,430,532
//-500,-761,534
//-322,571,750
//-466,-666,-811
//-429,-592,574
//-355,545,-477
//703,-491,-529
//-328,-685,520
//413,935,-424
//-391,539,-444
//586,-435,557
//-364,-763,-893
//807,-499,-711
//755,-354,-619
//553,889,-390

//--- scanner 2 ---
//649,640,665
//682,-795,504
//-784,533,-524
//-644,584,-595
//-588,-843,648
//-30,6,44
//-674,560,763
//500,723,-460
//609,671,-379
//-555,-800,653
//-675,-892,-343
//697,-426,-610
//578,704,681
//493,664,-388
//-671,-858,530
//-667,343,800
//571,-461,-707
//-138,-166,112
//-889,563,-600
//646,-828,498
//640,759,510
//-630,509,768
//-681,-892,-333
//673,-379,-804
//-742,-814,-386
//577,-820,562

//--- scanner 3 ---
//-589,542,597
//605,-692,669
//-500,565,-823
//-660,373,557
//-458,-679,-417
//-488,449,543
//-626,468,-788
//338,-750,-386
//528,-832,-391
//562,-778,733
//-938,-730,414
//543,643,-506
//-524,371,-870
//407,773,750
//-104,29,83
//378,-903,-323
//-778,-728,485
//426,699,580
//-438,-605,-362
//-469,-447,-387
//509,732,623
//647,635,-688
//-868,-804,481
//614,-800,639
//595,780,-596

//--- scanner 4 ---
//727,592,562
//-293,-554,779
//441,611,-461
//-714,465,-776
//-743,427,-804
//-660,-479,-426
//832,-632,460
//927,-485,-438
//408,393,-506
//466,436,-512
//110,16,151
//-258,-428,682
//-393,719,612
//-211,-452,876
//808,-476,-593
//-575,615,604
//-485,667,467
//-680,325,-822
//-627,-443,-432
//872,-547,-609
//833,512,582
//807,604,487
//839,-516,451
//891,-625,532
//-652,-548,-490
//30,-46,-14";

//var inputText =
//@"--- scanner 0 ---
//0,2,0
//4,1,0
//3,3,0

//--- scanner 1 ---
//-1,-1,0
//-5,0,0
//-2,1,0";


//var inputText =
//@"--- scanner 0 ---
//404,-588,-901
//528,-643,409
//-838,591,734
//390,-675,-793
//-537,-823,-458
//-485,-357,347
//-345,-311,381
//-661,-816,-575
//-876,649,763
//-618,-824,-621
//553,345,-567
//474,580,667
//-447,-329,318
//-584,868,-557
//544,-627,-890
//564,392,-477
//455,729,728
//-892,524,684
//-689,845,-530
//423,-701,434
//7,-33,-71
//630,319,-379
//443,580,662
//-789,900,-551
//459,-707,401

//--- scanner 1 ---
//686,422,578
//605,423,415
//515,917,-361
//-336,658,858
//95,138,22
//-476,619,847
//-340,-569,-846
//567,-361,727
//-460,603,-452
//669,-402,600
//729,430,532
//-500,-761,534
//-322,571,750
//-466,-666,-811
//-429,-592,574
//-355,545,-477
//703,-491,-529
//-328,-685,520
//413,935,-424
//-391,539,-444
//586,-435,557
//-364,-763,-893
//807,-499,-711
//755,-354,-619
//553,889,-390";

var sensors = inputText
    .Split("\n")
    .Where(s => !string.IsNullOrWhiteSpace(s))
    .Split(s => s.StartsWith("---"))
    .Skip(1)
    .Select(seq => seq.Select(item => ParsePoint(item)))
    .Select(sensorData => new Sensor(sensorData, true))
    .ToList();
(var points, var offsets) = MatchPoints(sensors, 12);
var allPoints = points.Distinct().OrderBy(p => p.X).ThenBy(p => p.Y).ThenBy(p => p.Z).ToList();

var offsetsWithZero = offsets.ToList();
offsetsWithZero.Add(new Point3(0, 0, 0));
var distances = offsetsWithZero.Cartesian(offsetsWithZero, (p1, p2) => p1.Manhattan(p2)).OrderByDescending(x => x).ToList();

var tmp = 4;


Point3 ParsePoint(string item)
{
    var p = item.Split(",").Select(s => int.Parse(s)).ToList();
    return new Point3(p[0], p[1], p[2]);
}


(IEnumerable<Point3>, IEnumerable<Point3>) MatchPoints(IEnumerable<Sensor> sensors, int minCount)
{
    (IEnumerable<Sensor> aligned, IEnumerable<Point3> offsets) = AlignSensors(sensors, minCount);
    return (aligned.SelectMany(p => p.SensorData), offsets);
}

(IEnumerable<Sensor>, IEnumerable<Point3>) AlignSensors(IEnumerable<Sensor> sensors, int minCount)
{
    var finished = sensors.Take(1).ToList();
    var remaining = sensors.Skip(1).ToList();
    var ret = new List<Point3>();
    for (int i = 0; i < finished.Count(); i++)
    {
        for (int j = remaining.Count() - 1; j >= 0; j--)
        {
            var sensor = remaining[j];
            var result = sensor.Matches(finished[i], minCount);
            if (result != null)
            {
                //throw new NotImplementedException();
                ret.Add(result.Value.offset);
                finished.Add(sensor.OffsetAndTransform(result.Value.offset, result.Value.transform));
                remaining.Remove(sensor);
            }
        }
    }

    return (finished, ret);
}